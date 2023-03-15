import React from 'react';
import ReactDOM from 'react-dom/client';
import UnityApp from './UnityApp';
import App from './App';
import './Page.css';

class MyHeader extends React.Component {
  render() {
    return <div></div>;
  }
}

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <div
      style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
      }}
    >
      <MyHeader />
      <UnityApp />
    </div>
    <App />
  </React.StrictMode>
);
