import {
  TextInput,
  Text,
  Paper,
  Divider,
  Stack,
  Group,
  Button,
  Loader,
} from '@mantine/core';
import { UserData, getUser } from '@/services/api';
import { DatePickerInput } from '@mantine/dates';
import { useEffect, useState } from 'react';
import { upperFirst, useLocalStorage } from '@mantine/hooks';
import { useRouter } from 'next/navigation';

export default function Profile() {
  const [user, setUser] = useState<UserData>();
  const [token, , removeValue] = useLocalStorage({ key: 'jwt' });
  const router = useRouter();

  useEffect(() => {
    if (token) {
      getUser().then((user) => {
        setUser(user);
      });
    }
  }, [token]);

  if (!user) {
    return <Loader color="blue" />;
  }

  async function onLogoutClick(e: React.FormEvent) {
    e.preventDefault();

    removeValue();
    router.push('/login');
  }

  return (
    <Paper radius="md" p="xl">
      <Text size="lg" fw={500}>
        Your profile information:
      </Text>

      <Divider my="lg" />

      <Stack w={300}>
        <TextInput
          disabled
          label="Username"
          placeholder="Username"
          value={user.username}
          radius="md"
        />

        <TextInput
          disabled
          label="Name"
          placeholder="Name"
          value={user.name}
          radius="md"
        />

        <TextInput
          disabled
          label="Surname"
          placeholder="Surname"
          value={user.surname}
          radius="md"
        />

        <DatePickerInput
          disabled
          label="Birthday"
          placeholder="Birthday"
          value={user.dateOfBirth}
        />

        <TextInput
          disabled
          label="Address"
          placeholder="Address"
          value={user.address}
          radius="md"
        />

        <Group justify="space-between" mt="xl">
          <Button radius="md" onClick={onLogoutClick}>
            {upperFirst('Logout')}
          </Button>
        </Group>
      </Stack>
    </Paper>
  );
}
