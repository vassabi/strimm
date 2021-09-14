function isSameOrigin() {
  try {
    return window.top && window.top.location && window.self === window.top;
  } catch (err) {
    return false;
  }
}

function initScript() {
  var strimmIframe = document.getElementById('strimm-iframe');

  if (strimmIframe) {
    strimmIframe.onload = function (evt) {
      // https://stackoverflow.com/a/38459639/2343074
      if (evt.target.src !== '') {
        setIframeWindowDomainHash();
      }
    };
  }
}

function iframeMessageListener(event) {
  if (event.data && event.data.hasOwnProperty('type')) {
    switch (event.data.type) {
      case 'IFRAME_CHANGE_HEIGHT':
        iframeChangeHeightHandler(event);

        break;

      case 'IFRAME_FULLSCREEN_TOGGLED':
        iframeFullscreenToggled(event);

        break;

      default:
        break;
    }
  }
}

function iframeChangeHeightHandler(event) {
  var embeddedDiv = document.getElementById('embeddedDiv');
  var iframeHeight = 0;

  if (embeddedDiv.getAttribute('data-fullscreen') === 'true') {
    return;
  }

  iframeHeight = event.data.data.iframeHeight;
  embeddedDiv.style.height = iframeHeight + 'px';
}

function iframeFullscreenToggled(event) {
  var embeddedDiv = document.getElementById('embeddedDiv');
  var isFullscreenEnabled = event.data.data.isFullscreenEnabled;

  if (isFullscreenEnabled) {
    embeddedDiv.style.position = 'fixed';
    embeddedDiv.style.top = '0';
    embeddedDiv.style.left = '0';
    embeddedDiv.style.height = '100%';
    embeddedDiv.style.width = '100%';
    embeddedDiv.style.zIndex = '999999999';

    embeddedDiv.setAttribute('data-fullscreen', 'true');
  } else {
    embeddedDiv.style.position = 'relative';
    embeddedDiv.style.removeProperty('top');
    embeddedDiv.style.removeProperty('left');
    embeddedDiv.style.removeProperty('width');
    embeddedDiv.style.removeProperty('z-index');

    embeddedDiv.removeAttribute('data-fullscreen');
  }
}

function setIframeWindowDomainHash() {
  var embeddedDiv = document.getElementById('embeddedDiv');
  var strimmIframe = document.getElementById('strimm-iframe');
  // https://stackoverflow.com/a/4386514/2343074
  var clonedIframe = strimmIframe.cloneNode(true);
  var strimmIframeSrc = strimmIframe.src;
  var locationHash = !isSameOrigin()
    ? `double-${document.referrer}`
    : `single-${window.location.href}`;

  console.log('location hash: ', locationHash);

  strimmIframe.parentNode.removeChild(strimmIframe);

  window.addEventListener('message', iframeMessageListener, false);

  clonedIframe.src = `${strimmIframeSrc}#${locationHash}`;
  clonedIframe.allow = 'autoplay';

  embeddedDiv.appendChild(clonedIframe);
}

document.addEventListener('DOMContentLoaded', function () {
  initScript();
});
