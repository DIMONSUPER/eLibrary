'use client';

import {
  ApiResponse,
  Author,
  deleteAuthor,
  getAuthor,
  updateAuthor,
} from '@/services/api';
import {
  TextInput,
  Paper,
  Group,
  Button,
  Stack,
  Notification,
  Loader,
} from '@mantine/core';
import { DatePickerInput } from '@mantine/dates';
import { useForm } from '@mantine/form';
import { useValidatedState } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import DataLoadingView from '../DataLoadingView';

export default function AuthorDetails({ authorId }: { authorId: number }) {
  const router = useRouter();
  const [authorResponse, setAuthorResponse] = useState<ApiResponse<Author>>();
  const [opened, setOpened] = useState(false);
  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );

  const { values, getInputProps, setValues } = useForm<Author>({
    initialValues: {
      id: authorId,
      firstName: '',
      lastName: '',
      dateOfBirth: new Date(2000, 11, 18),
    },

    validate: {
      firstName: (val) => (val.length > 0 ? null : 'This field cant be empty'),
      lastName: (val) => (val.length > 0 ? null : 'This field cant be empty'),
    },
  });

  useEffect(() => {
    getAuthor(authorId).then((authorResponse) => {
      setAuthorResponse(authorResponse);
      setValues({
        firstName: authorResponse.data?.firstName,
        lastName: authorResponse.data?.lastName,
        dateOfBirth: authorResponse.data?.dateOfBirth,
      });
    });
  }, [authorId, setValues]);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const updateResponse = await updateAuthor(values);
    if (updateResponse.statusCode == 200) {
      setErrorMessage('');
      setOpened(true);
      setTimeout(() => setOpened(false), 2000);
    } else {
      setErrorMessage(updateResponse.errors.join('<br /><br />'));
    }
  }

  async function onClick(e: React.FormEvent) {
    e.preventDefault();

    const deleteResponse = await deleteAuthor(values.id);

    if (deleteResponse.statusCode == 200) {
      setErrorMessage('');
      router.back();
    } else {
      setErrorMessage(deleteResponse.errors.join('<br /><br />'));
    }
  }

  return (
    <DataLoadingView apiResponse={authorResponse}>
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

            {opened && (
              <Notification
                withCloseButton={false}
                title="Author was successfully updated"
                color="green"
              />
            )}

            {!valid && (
              <Notification withCloseButton={false} color="red">
                <p dangerouslySetInnerHTML={{ __html: value }} />
              </Notification>
            )}

            <Group justify="space-between" mt="lg">
              <Button type="submit" radius="md">
                Update
              </Button>
              <Button color="red" type="button" radius="md" onClick={onClick}>
                Delete (with all books)
              </Button>
            </Group>
          </Stack>
        </form>
      </Paper>
    </DataLoadingView>
  );
}
