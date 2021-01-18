import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Home from './components/Pages/Home/Home';
import NavigationMenu from './components/NavigationMenu/NavigationMenu';
import styles from './App.module.css';
import Footer from './components/Footer/Footer';
import Schedule from './components/Pages/Schedule/Schedule';

function App() {
  return (
    <BrowserRouter>
      <div className={styles.appWrapper}>
        <header>
          <NavigationMenu />
        </header>
        <div className={styles.appWrapperContent}>
          <div>
            <Route path="/" exact={true} render={() => <Home /> } />
            <Route path="/schedule" exact={true} render={() => <Schedule /> } />
          </div> 
        </div>
        <footer>
          <Footer />
        </footer>
      </div>
    </BrowserRouter>
  );
}

export default App;
