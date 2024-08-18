export function importedScripts() {
    let src = document.createElement('script');
  src.setAttribute('src', 'js/main.js');
  document.body.appendChild(src);
}