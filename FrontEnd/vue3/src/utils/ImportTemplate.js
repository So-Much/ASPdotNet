import { onMounted, onUnmounted } from "vue";
function usePhotographerTemplate() {
  const templatedScripts = [];
  const templatedStyles = [];
  onMounted(() => {
    // Import JS files:
    const requireJsFiles = require.context('../../public/js/photographerTemplate', false, /\.js$/);
    const jquery = require.context('../../public/js/photographerTemplate', false, /jquery/);
    jquery.keys().forEach(filepath => {
      let src = document.createElement('script');
      src.setAttribute('src', filepath.replace('./', 'js/photographerTemplate/'));
      document.body.appendChild(src);
      templatedScripts.push(src);
    });
    console.log('Jquery is imported successfully')
    requireJsFiles.keys().forEach(filepath => {
      if(filepath.includes('jquery')) return;
      let src = document.createElement('script');
      src.setAttribute('src', filepath.replace('./', 'js/photographerTemplate/'));
      document.body.appendChild(src);
      templatedScripts.push(src);
    });
    console.log('Scripts is imported successfully')
    // Import CSS files
    const requireCssFiles = require.context('../../public/css/photographerTemplate', false, /\.css$/);
    requireCssFiles.keys().forEach(filepath => {
      let link = document.createElement('link');
      link.setAttribute('rel', 'stylesheet');
      link.setAttribute('href', filepath.replace('./', 'css/photographerTemplate/'));
      document.head.appendChild(link);
      templatedStyles.push(link);
    });
    console.log('Scripts Files path:')
    templatedScripts.forEach(e => console.log(e.src));
    console.log('STyle Files path:')
    templatedStyles.forEach(e => console.log(e.href));
    
  });
  onUnmounted(() => {
    // Remove JS files
    templatedScripts.forEach(element => {
      document.body.removeChild(element);
    });

    // Remove CSS files
    templatedStyles.forEach(element => {
      document.head.removeChild(element);
    });
  });
}
export {
  usePhotographerTemplate
}