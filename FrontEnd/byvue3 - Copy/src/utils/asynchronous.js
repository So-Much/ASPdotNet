
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
    // 1 bug: when route != /(localhost:8080/), although route is: localhost:8080/x
    //  the main.js will not inject because link img is localhost:8080/x/:link
    // So get the bug resource is not found
    injectMainJS,
    removeMainJS
}