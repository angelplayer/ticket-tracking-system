import { useContext } from "react";
import { AuthenticationContext } from "./auth-provider";

export const useAuth = () => {
  const context= useContext(AuthenticationContext);
  return {
    ...context,
    isAuthenticated: context.isAuthenticated
  }
}