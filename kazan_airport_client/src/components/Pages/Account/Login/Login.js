import React from 'react';
import { Route } from 'react-router-dom';
import styles from './Login.module.css'
import SignInModule from './SignInModule';
import SignUpModule from './SignUpModule';

const Login = () => {
    return(
        <div className={styles.loginContainer}>
            <Route path="/login" exact={true} render={() => <SignInModule /> } />
            <Route path="/login/register" exact={true} render={() => <SignUpModule /> } />
        </div>
    );
}

export default Login;