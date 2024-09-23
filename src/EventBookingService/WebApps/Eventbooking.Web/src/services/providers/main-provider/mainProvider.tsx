import { AuthProvider } from "react-oidc-context";
import { oidcConfig } from "./configs/oidcConfig";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "./configs/queryClient";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

interface MainProviderProps {
    children: React.ReactNode;
}

const MainProvider: React.FC<MainProviderProps> = ({children}) => {
  return (
    <AuthProvider {...oidcConfig}>
        <QueryClientProvider client={queryClient}>
            {children}
            <ReactQueryDevtools initialIsOpen={false} />
        </QueryClientProvider>
    </AuthProvider>
  );
};

export default MainProvider;