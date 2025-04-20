function PageReload() {
    location.reload();
}

window.registerTouchEnd = () => {
    document.addEventListener("touchend", (event) => {
        if (event.touches.length > 1) {
            location.reload();
        }
    });
};