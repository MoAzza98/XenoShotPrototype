import React, { Fragment } from 'react';
import { Unity, useUnityContext } from 'react-unity-webgl';
import './UnityApp.css';

function UnityApp() {
  const { unityProvider, isLoaded, loadingProgression, requestFullscreen } =
    useUnityContext({
      loaderUrl: 'Omegabuild/Build/Omegabuild.loader.js',
      dataUrl: 'Omegabuild/Build/Omegabuild.data',
      frameworkUrl: 'Omegabuild/Build/Omegabuild.framework.js',
      codeUrl: 'Omegabuild/Build/Omegabuild.wasm',
    });

  function handleClickEnterFullscreen() {
    requestFullscreen(true);
  }

  return (
    <div className="container">
      <div
        className="UnityApp"
        style={{ width: `${loadingProgression * 100}%` }}
      />
      <Fragment>
        <Unity
          style={{
            visibility: isLoaded ? 'visible' : 'hidden',
            width: 1920,
            height: 1080,
          }}
          unityProvider={unityProvider}
          devicePixelRatio={window.devicePixelRatio}
        />
      </Fragment>{' '}
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <button className="button button1" onClick={handleClickEnterFullscreen}>
          Enter Fullscreen
        </button>
      </div>
      <p>
        It is recommended to play XenoShot in fullscreen for the intended
        experience.
      </p>
    </div>
  );
}

export default UnityApp;
