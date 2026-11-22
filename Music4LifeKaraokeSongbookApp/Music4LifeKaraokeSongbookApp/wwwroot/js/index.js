window.scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'auto' });
}

window.getRequesterName = () => {
    return localStorage.getItem("_singerName");
};

window.setRequesterName = (name) => {
    localStorage.setItem("_singerName", name);
}