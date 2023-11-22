import '@mantine/core/styles.css';

import NextLink from 'next/link';
import { Button } from '@mantine/core';

type LinkProps = {
  href: `/${string}`;
  label: string;
};

export default function Link({ href, label }: LinkProps) {
  return (
    <Button mt="sm" variant="light" component={NextLink} href={href}>
      {label}
    </Button>
  );
}
