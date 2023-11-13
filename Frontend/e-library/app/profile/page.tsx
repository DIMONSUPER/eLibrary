'use client'

import Profile from '@/components/Profile';
import { AuthProvider } from '@/modules/auth/AuthProvider';

export default function Page() {

  return(
    <AuthProvider>
      <Profile/>
    </AuthProvider>
  );
}