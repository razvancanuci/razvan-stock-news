export interface User {
    username: string;
    password: string;
}

export interface UserRole {
    username: string;
    role: string;
}

export interface UserId {
    id: string;
    username: string;
    role: string;
}

export interface TokenModel {
    token: string;
    role: string;
}

export interface CodeModel {
    code: string;
}