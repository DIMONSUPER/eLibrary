'use client';

import { Author, createAuthor } from '@/services/api';
import {
  TextInput,
  Paper,
  Group,
  Button,
  Stack,
  Notification,
} from '@mantine/core';
import { DatePickerInput } from '@mantine/dates';
import { useForm } from '@mantine/form';
import { useValidatedState } from '@mantine/hooks';
import { useRouter } from 'next/navigation';

export default function CreateAuthor() {
  const router = useRouter();

  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );
  const { values, getInputProps } = useForm<Author>({
    initialValues: {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: new Date(2000, 11, 18),
    },
  });

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const createResponse = await createAuthor(values);

    if (createResponse.statusCode == 201) {
      setErrorMessage('');
    } else {
      setErrorMessage(createResponse.errors.join('<br /><br />'));
    }

    router.back();
  }

  return (
    <Paper radius="md" p="xl">
      <form onSubmit={onSubmit}>
        <Stack w={300}>
          <TextInput
            required
            label="Name"
            placeholder="Name"
            radius="md"
            {...getInputProps('firstName')}
          />

          <TextInput
            required
            label="Surname"
            placeholder="Surname"
            radius="md"
            {...getInputProps('lastName')}
          />

          <DatePickerInput
            required
            label="Birthday"
            placeholder="Birthday"
            {...getInputProps('dateOfBirth')}
          />

          {!valid && (
            <Notification withCloseButton={false} color="red">
              <p dangerouslySetInnerHTML={{ __html: value }} />
            </Notification>
          )}

          <Group justify="space-between" mt="lg">
            <Button type="submit" radius="md">
              Create
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
