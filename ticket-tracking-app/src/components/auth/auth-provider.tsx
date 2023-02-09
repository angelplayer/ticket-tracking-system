import { createContext, useState } from "react";
import { destroyUserToken, saveUserToken } from "./localstorage";


export const AuthenticationContext = createContext({
	signIn: (userToken) => {},
	signOut: () => {},
	isAuthenticated: false
});


export const AuthenticationProvider = ( { children }) => {

	const [state, setState] = useState(false);

	const authenticationValue = {
		isAuthenticated: state,

		signIn(userToken) {
			saveUserToken(userToken);
			setState(true);
		}, 

		signOut() {
			destroyUserToken();
      setState(false);
		}
	}

	return (
		<AuthenticationContext.Provider value={authenticationValue}>{children}</AuthenticationContext.Provider>
  )
}

