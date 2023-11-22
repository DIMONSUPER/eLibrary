import { useLocalStorage, useValidatedState } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  Button,
  Divider,
  Notification,
  Stack,
} from '@mantine/core';
import { login as loginApi } from '@/services/api';
import { useRouter } from 'next/navigation';

export default function LoginForm() {
  const router = useRouter();
  const [, setValue] = useLocalStorage({ key: 'jwt' });
  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const loginResponse = await loginApi(form.values);

    if (loginResponse?.data) {
      setValue(loginResponse.data);
      setErrorMessage('');
      router.push('/profile');
    } else {
      setErrorMessage(loginResponse.errors.join('<br /><br />'));
    }
  }

  const form = useForm({
    initialValues: {
      username: '',
      password: '',
    },
  });

  return (
    <Paper radius="md" p="xl" w="100%">
      <Text size="lg" fw={500}>
        Welcome to bg-local.com
      </Text>

      <Divider my="lg" />

      <form onSubmit={onSubmit}>
        <Stack w={300}>
          <TextInput
            required
            label="Username"
            placeholder="Username"
            radius="md"
            {...form.getInputProps('username')}
          />

          <PasswordInput
            required
            label="Password"
            placeholder="Password"
            radius="md"
            {...form.getInputProps('password')}
          />

          {!valid && (
            <Notification withCloseButton={false} color="red">
              <p dangerouslySetInnerHTML={{ __html: value }} />
            </Notification>
          )}

          <Group justify="space-between" mt="xl">
            <Button type="submit" radius="md">
              Login
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
