//window.location.assign(`${window.location.href}`);

const windowHref =  window.location.href;

window.location.assign(`${windowHref.substring(0,windowHref.length-10)}Patients.html`);