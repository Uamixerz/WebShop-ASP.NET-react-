export interface ILogin {
    email: string,
    password: string,
    firstName: string,
    lastName:string,
    confirmPassword:string,
    image: File | null
}
