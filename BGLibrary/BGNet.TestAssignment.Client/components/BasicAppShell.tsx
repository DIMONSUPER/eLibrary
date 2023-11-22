'use client'

import '@mantine/core/styles.css';

import { AppShell, AppShellMain } from '@mantine/core';
import Navbar from './Navbar';

export default function BasicAppShell({ children }: { children: React.ReactNode }) {
  return (
    <AppShell
      navbar={{ width: 200, breakpoint: 'sm'}}
      padding='md'>
      <Navbar/>
      <AppShellMain>{children}</AppShellMain>
    </AppShell>
  );
}