import { User } from "oidc-client-ts"

export function getUser() {
    const oidcStorage = sessionStorage.getItem(`oidc.user:${import.meta.env.VITE_AUTHORITY}:${import.meta.env.VITE_CLIENT_ID}`)

    if (!oidcStorage) {
        return null;
    }

    return User.fromStorageString(oidcStorage);
}
