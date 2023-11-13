'use client'

import { AppShellNavbar } from '@mantine/core';
import Link from '@/components/Link';
import { useLocalStorage } from '@mantine/hooks';

export default function Navbar() {

  const [value] = useLocalStorage({key: 'jwt'});

  return (
  <AppShellNavbar p='md'>
     {value ? <>
          <Link label='Profile' href='/profile'/>
          <Link label='Books' href='/books'/>
          </> : <>
          <Link label='Login' href='/login'/>
          <Link label='Register' href='/register'/>
          </>}
  </AppShellNavbar>
  );
}