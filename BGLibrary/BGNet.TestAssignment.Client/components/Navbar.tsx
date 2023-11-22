'use client';

import { AppShellNavbar } from '@mantine/core';
import Link from '@/components/Link';
import { useLocalStorage } from '@mantine/hooks';

export default function Navbar() {
  return (
    <AppShellNavbar p="md">
      <Link label="Profile" href="/profile" />
      <Link label="Books" href="/books" />
      <Link label="Login" href="/login" />
      <Link label="Register" href="/register" />
    </AppShellNavbar>
  );
}
