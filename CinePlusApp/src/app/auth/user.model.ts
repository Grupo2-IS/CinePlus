export class User {
    id: number;
    firstName: string;
    userName: string;
    role: string;
    jwtToken?: string;
    constructor(id, firstName, userName, role) {
        this.id = id;
        this.firstName = firstName;
        this.userName = userName;
        this.role = role;
    }
}



// {
//     "id": 7,
//     "firstName": "admin",
//     "username": "el jefe",
//     "jwtToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjciLCJuYmYiOjE2MjQzNDA1OTAsImV4cCI6MTYyNDM0MTQ5MCwiaWF0IjoxNjI0MzQwNTkwfQ.Lwe7DIXGjddoOseb27WIO2UtreChIPxn28a9RGSK8GA",
//     "role": "Admin"
// }