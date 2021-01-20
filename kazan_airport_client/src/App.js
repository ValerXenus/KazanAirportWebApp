import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Home from './components/Pages/Home/Home';
import NavigationMenu from './components/NavigationMenu/NavigationMenu';
import styles from './App.module.css';
import Footer from './components/Footer/Footer';
import Schedule from './components/Pages/Schedule/Schedule';
import Services from './components/Pages/ServicesComponents/Services';
import HowToGet from './components/Pages/HowToGet/HowToGet';
import TerminalsScheme from './components/Pages/TerminalsSchemeComponents/TerminalsScheme';
import AboutComponents from './components/Pages/AboutComponents/AboutComponents';
import Login from './components/Pages/Account/Login/Login';

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
            <Route path="/services" render={() => <Services /> } />
            <Route path="/howtoget" render={() => <HowToGet /> } />
            <Route path="/terminals" render={() => <TerminalsScheme /> } />
            <Route path="/about" render={() => <AboutComponents /> } />
            <Route path="/login" render={() => <Login /> } />
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
