import { useValidatedState } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import { DatePickerInput } from '@mantine/dates';
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
import { useRouter } from 'next/navigation';
import { register } from '@/services/api';

export default function RegistrationForm() {
  const router = useRouter();
  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const registerResponse = await register(form.values);

    if (registerResponse.statusCode == 201) {
      setErrorMessage('');
      router.push('/login');
    } else {
      setErrorMessage(registerResponse.errors.join('<br /><br />'));
    }
  }

  const form = useForm({
    initialValues: {
      username: '',
      password: '',
      firstName: '',
      lastName: '',
      dateofbirth: new Date(2000, 11, 18),
      address: '',
    },
  });

  return (
    <Paper radius="md" p="xl">
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

          <TextInput
            required
            label="First Name"
            placeholder="First Name"
            radius="md"
            {...form.getInputProps('firstName')}
          />

          <TextInput
            required
            label="Last Name"
            placeholder="Last Name"
            radius="md"
            {...form.getInputProps('lastName')}
          />

          <DatePickerInput
            required
            label="Birthday"
            placeholder="Birthday"
            {...form.getInputProps('dateofbirth')}
          />

          <TextInput
            required
            label="Address"
            placeholder="Address"
            radius="md"
            {...form.getInputProps('address')}
          />
          {!valid && (
            <Notification
              title="Please fix these issues:"
              withCloseButton={false}
              color="red"
            >
              <p dangerouslySetInnerHTML={{ __html: value }} />
            </Notification>
          )}

          <Group justify="space-between" mt="lg">
            <Button type="submit" radius="md">
              Register
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
