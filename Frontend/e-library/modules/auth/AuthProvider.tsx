'use client';

import { ReactElement, useEffect, useState } from 'react';
import { AuthContext } from './AuthContext';

export function AuthProvider({ children }: { children: ReactElement }) {
  const [jwt, setJwt] = useState('');

  useEffect(() => {
    setJwt(localStorage.getItem('jwt') ?? '');
  }, []);

  const login = (jwt: string) => {
    setJwt(jwt);
    localStorage.setItem('jwt', jwt);
  };

  const logout = () => {
    setJwt('');
    localStorage.setItem('jwt', '');
  };

  return (
    <AuthContext.Provider value={{ jwt, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}
