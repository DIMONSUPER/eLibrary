import '@mantine/core/styles.css';
import '@mantine/dates/styles.css';

import { MantineProvider, ColorSchemeScript, createTheme } from '@mantine/core';
import BasicAppShell from '@/components/BasicAppShell';

const theme = createTheme({
  fontFamily: 'Open Sans, sans-serif',
});

export const metadata = {
  title: 'eLibrary',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <head>
        <ColorSchemeScript defaultColorScheme="dark" />
      </head>
      <body>
        <MantineProvider theme={theme} defaultColorScheme="dark">
          <BasicAppShell>{children}</BasicAppShell>
        </MantineProvider>
      </body>
    </html>
  );
}
