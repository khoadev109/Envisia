export interface LoginRequest {
    userName: string;
    password: string;
    tenantIndentifier?: string;
}

export interface TokenResponse {
    accessToken: string;
    refreshToken: string;
    userIdentifier: string;
    tenantIdentifier: string;
}
