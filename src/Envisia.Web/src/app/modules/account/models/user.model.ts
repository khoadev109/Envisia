export interface AppUser {
    displayName: string,
    email: string,
    firstName: string,
    lastName: string,
    userIdentifier: string
}

export interface AppUserProfileRequest extends  AppUser {

}
