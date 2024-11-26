
function injectMainJS() {
    let src = document.createElement('script');
    src.setAttribute('src', `${window.location.origin}/js/main.js`);
    document.body.appendChild(src);
}
function removeMainJS() {
    let src = document.querySelector(`script[src="${window.location.origin}/js/main.js"]`);
    document.body.removeChild(src);
}
export {
    injectMainJS,
    removeMainJS
}