import { createContext, useState } from 'react';

type AuthContextParams = {
  jwt: string,
  login(jwt:string): void,
  logout(): void,
}

export const AuthContext = createContext<AuthContextParams>({
  jwt: '',
  login:() => {},
  logout:() => {},
});