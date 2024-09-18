import { useAuth } from 'react-oidc-context';
import './App.css'
import Notification from "./components/Notification";
import CheckoutTest from './components/CheckoutTest';

function App() {
    const auth = useAuth();

    switch (auth.activeNavigator) {
        case "signinSilent":
            return <div>Signing you in...</div>;
        case "signoutRedirect":
            return <div>Signing you out...</div>;
    }

    if (auth.isLoading) {
        return <div>Loading...</div>;
    }

    if (auth.error) {
        return <div>Oops... {auth.error.message}</div>;
    }

    if (auth.isAuthenticated) {
        return (
            <div>
                <button onClick={() => console.log(auth.user?.access_token)}>access_token</button>
                <Notification />
                {/* <Group /> */}

                <button onClick={() => void auth.removeUser()}>Log out</button>
                <CheckoutTest />
            </div>
        );
    }

    return <button onClick={() => void auth.signinRedirect()}>Log in</button>;
}

export default App
