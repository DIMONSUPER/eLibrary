import {
  TextInput,
  Text,
  Paper,
  Divider,
  Stack,
  Group,
  Button,
} from '@mantine/core';
import { ApiResponse, UserData, getUser } from '@/services/api';
import { DatePickerInput } from '@mantine/dates';
import { useEffect, useState } from 'react';
import { useLocalStorage } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import DataLoadingView from '../DataLoadingView';

export default function Profile() {
  const [userResponse, setUserResponse] = useState<ApiResponse<UserData>>();
  const [, setValue] = useLocalStorage({ key: 'jwt' });
  const router = useRouter();

  useEffect(() => {
    getUser().then((userResponse) => setUserResponse(userResponse));
  }, []);

  async function onLogoutClick(e: React.FormEvent) {
    e.preventDefault();

    setValue('');
    router.push('/login');
  }

  return (
    <DataLoadingView apiResponse={userResponse}>
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
            value={userResponse?.data?.username}
            radius="md"
          />

          <TextInput
            disabled
            label="Name"
            placeholder="Name"
            value={userResponse?.data?.firstName}
            radius="md"
          />

          <TextInput
            disabled
            label="Surname"
            placeholder="Surname"
            value={userResponse?.data?.lastName}
            radius="md"
          />

          <DatePickerInput
            disabled
            label="Birthday"
            placeholder="Birthday"
            value={userResponse?.data?.dateOfBirth}
          />

          <TextInput
            disabled
            label="Address"
            placeholder="Address"
            value={userResponse?.data?.address}
            radius="md"
          />

          <Group justify="space-between" mt="xl">
            <Button color="red" radius="md" onClick={onLogoutClick}>
              Logout
            </Button>
          </Group>
        </Stack>
      </Paper>
    </DataLoadingView>
  );
}
