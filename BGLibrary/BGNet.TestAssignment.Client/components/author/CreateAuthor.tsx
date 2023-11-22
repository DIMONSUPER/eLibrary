'use client';

import { Author, createAuthor, deleteAuthor } from '@/services/api';
import { TextInput, Paper, Group, Button, Stack, Loader } from '@mantine/core';
import { DatePickerInput } from '@mantine/dates';
import { useForm } from '@mantine/form';
import { useRouter } from 'next/navigation';

export default function CreateAuthor() {
  const router = useRouter();

  const { values, getInputProps } = useForm<Author>({
    initialValues: {
      id: 0,
      name: '',
      surname: '',
      dateOfBirth: new Date(2000, 11, 18),
    },

    validate: {
      name: (val) => (val.length > 0 ? null : 'This field cant be empty'),
      surname: (val) => (val.length > 0 ? null : 'This field cant be empty'),
    },
  });

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      await createAuthor(values);

      router.back();
    } catch (error) {
      console.error('createAuthor failed:', error);
    }
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
            {...getInputProps('name')}
          />

          <TextInput
            required
            label="Surname"
            placeholder="Surname"
            radius="md"
            {...getInputProps('surname')}
          />

          <DatePickerInput
            required
            label="Birthday"
            placeholder="Birthday"
            {...getInputProps('dateOfBirth')}
          />

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
