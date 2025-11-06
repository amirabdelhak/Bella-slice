window.addEventListener('scroll', () => {
    const navbar = document.querySelector('.navbar-pizza');
    if (!navbar) return;
    if (window.scrollY > 50) {
        navbar.classList.add('scrolled');
    } else {
        navbar.classList.remove('scrolled');
    }
});

// Active nav-link based on sections in view or current path/hash
document.addEventListener('DOMContentLoaded', function () {
    const navLinks = document.querySelectorAll('.navbar-pizza .nav-link');
    const sections = document.querySelectorAll('section[id]');

    if (!navLinks || navLinks.length === 0) return;

    const removeActive = () => navLinks.forEach(l => l.classList.remove('active'));
    const setActiveBySelector = (selector) => {
        removeActive();
        const link = document.querySelector('.navbar-pizza .nav-link[href="' + selector + '"]');
        if (link) link.classList.add('active');
    };

    // manualActive: when user clicks a nav link we keep that link active until they click another
    let manualActive = false;

    // restore manual selection from session (optional)
    const stored = sessionStorage.getItem('navManualActive');
    if (stored) {
        manualActive = true;
        setActiveBySelector(stored);
    }

    // Use IntersectionObserver when sections exist; but do not override manual selection
    if ('IntersectionObserver' in window && sections.length > 0) {
        const observer = new IntersectionObserver((entries) => {
            if (manualActive) return; // don't change active if user manually selected
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    setActiveBySelector('#' + entry.target.id);
                }
            });
        }, { threshold: 0.55 });

        sections.forEach(s => observer.observe(s));
    } else {
        // fallback to hash or pathname (only if not manual)
        if (!manualActive) {
            const hash = location.hash;
            if (hash) setActiveBySelector(hash);
            else {
                const path = location.pathname.toLowerCase();
                if (path === '/' || path.includes('home')) setActiveBySelector('#home');
                else if (path.includes('menu')) setActiveBySelector('#menu');
            }
        }
    }

    // clicking a nav link should immediately show active state and become manual
    navLinks.forEach(link => {
        link.addEventListener('click', function () {
            manualActive = true;
            const href = this.getAttribute('href');
            // store selection so it persists across same-origin navigation during the session
            try { sessionStorage.setItem('navManualActive', href); } catch (e) { }
            removeActive();
            this.classList.add('active');
        });
    });

    // update on hashchange only when not manual
    window.addEventListener('hashchange', () => {
        if (!manualActive) {
            const h = location.hash;
            if (h) setActiveBySelector(h);
        }
    });

    // helper to update cart badge
    const cartIcon = document.getElementById('cartIcon');
    const updateCartBadge = (count) => {
        let badge = document.getElementById('cartCountBadge');
        if (!badge) {
            badge = document.createElement('span');
            badge.id = 'cartCountBadge';
            badge.className = 'badge bg-danger rounded-pill position-absolute top-0 start-100 translate-middle';
            badge.textContent = count;
            if (cartIcon) cartIcon.appendChild(badge);
        } else {
            badge.textContent = count;
        }
        if (badge) badge.style.display = count > 0 ? 'inline-block' : 'none';
    };

    //// AJAX Add to Cart handling (sends antiforgery token and credentials)
    //document.querySelectorAll('.add-to-cart-form').forEach(form => {
    //    form.addEventListener('submit', function (e) {
    //        e.preventDefault();

    //        // determine target action (fallback to /Cart/AddToCart)
    //        const action = (this.action && this.action.trim().length > 0) ? this.action : (window.location.origin + '/Cart/AddToCart');

    //        const fd = new FormData(this);
    //        // try to get antiforgery token from the form
    //        const tokenInput = this.querySelector('input[name="__RequestVerificationToken"]');
    //        const token = tokenInput ? tokenInput.value : (document.querySelector('input[name="__RequestVerificationToken"]') ? document.querySelector('input[name="__RequestVerificationToken"]').value : '');

    //        const headers = {
    //            'X-Requested-With': 'XMLHttpRequest'
    //        };
    //        if (token) headers['RequestVerificationToken'] = token;

    //        fetch(action, {
    //            method: 'POST',
    //            credentials: 'same-origin',
    //            headers: headers,
    //            body: fd
    //        }).then(resp => {
    //            if (!resp.ok) throw new Error('Network response was not ok');
    //            return resp.json();
    //        }).then(data => {
    //            if (data && (data.count !== undefined)) {
    //                updateCartBadge(data.count);
    //            } else {
    //                // fallback: reload
    //                window.location.reload();
    //            }
    //        }).catch(err => {
    //            // on error fallback to normal submit
    //            this.submit();
    //        });
    //    });
    //});

});
