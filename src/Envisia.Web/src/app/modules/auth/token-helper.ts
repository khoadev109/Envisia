import { environment } from "src/environments/environment";
import { TokenResponse } from "./models/login.model";

export function setTokenAndIdentifiers(tokenResponse: TokenResponse) {
    removeTokensAndIdentifiers();
    localStorage.setItem(environment.tokenStorage, tokenResponse.accessToken);
    localStorage.setItem(environment.refreshTokenStorage, tokenResponse.refreshToken);
}

export function getAccessToken() {
    return localStorage.getItem(environment.tokenStorage);
}

export function getRefreshToken() {
    return localStorage.getItem(environment.refreshTokenStorage) || '';
}

export function removeTokensAndIdentifiers() {
    localStorage.removeItem(environment.tokenStorage);
    localStorage.removeItem(environment.refreshTokenStorage);
}
