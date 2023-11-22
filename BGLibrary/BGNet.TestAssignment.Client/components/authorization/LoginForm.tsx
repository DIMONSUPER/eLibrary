import { upperFirst, useLocalStorage } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  Button,
  Divider,
  Stack,
} from '@mantine/core';
import { login as loginApi } from '@/services/api';
import { useRouter } from 'next/navigation';

export default function LoginForm() {
  const router = useRouter();
  const [, setValue] = useLocalStorage({ key: 'jwt' });

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      const jwt = await loginApi(form.values);

      setValue(jwt);

      router.push('/profile');
    } catch (error) {
      console.error('Login failed:', error);
    }
  }

  const form = useForm({
    initialValues: {
      username: '',
      password: '',
    },

    validate: {
      username: (val) =>
        val.length < 6 ? 'Username should include at least 6 characters' : null,
      password: (val) =>
        val.length < 6 ? 'Password should include at least 6 characters' : null,
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

          <Group justify="space-between" mt="xl">
            <Button type="submit" radius="md">
              {upperFirst('login')}
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
