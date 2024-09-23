import { AuthProvider } from "react-oidc-context";
import { oidcConfig } from "./configs/oidcConfig";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "./configs/queryClient";

interface MainProviderProps {
    children: React.ReactNode;
}

const MainProvider: React.FC<MainProviderProps> = ({children}) => {
  return (
    <AuthProvider {...oidcConfig}>
        <QueryClientProvider client={queryClient}>
            {children}
        </QueryClientProvider>
    </AuthProvider>
  );
};

export default MainProvider;