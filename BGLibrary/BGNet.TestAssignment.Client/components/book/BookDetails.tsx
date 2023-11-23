'use client';

import {
  ApiResponse,
  Author,
  deleteBook,
  getAuthors,
  getBook,
  updateBook,
} from '@/services/api';
import {
  TextInput,
  Paper,
  Group,
  Button,
  Stack,
  NativeSelect,
  NumberInput,
  Notification,
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { upperFirst, useValidatedState } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import DataLoadingView from '../DataLoadingView';

export default function BookDetails({ bookId }: { bookId: number }) {
  const router = useRouter();

  const [authorsResponse, setAuthorsResponse] =
    useState<ApiResponse<Author[]>>();
  const [authorNames, setAuthorNames] = useState<string[]>([]);
  const [authorValue, setAuthorValue] = useState('');
  const [opened, setOpened] = useState(false);
  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );

  const { values, getInputProps, setValues } = useForm({
    initialValues: {
      id: bookId,
      title: '',
      publicationYear: 2000,
      genre: '',
      authorId: 0,
    },
  });

  useEffect(() => {
    getBook(bookId)
      .then((bookResponse) => {
        const initalAuthorName = `${bookResponse.data?.author?.firstName} ${bookResponse.data?.author?.lastName}`;
        setAuthorValue(initalAuthorName);
        setValues({
          title: bookResponse.data?.title,
          publicationYear: bookResponse.data?.publicationYear,
          genre: bookResponse.data?.genre,
          authorId: bookResponse.data?.author?.id,
        });
      })
      .then(() => {
        getAuthors().then((authorsResponse) => {
          setAuthorsResponse(authorsResponse);
          if (authorsResponse?.data && authorsResponse.data.length > 0) {
            setAuthorNames(authorsResponse.data.map((x) => x.fullName!));
          }
        });
      });
  }, [bookId, setValues]);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const updateResponse = await updateBook(values);

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

    const deleteResponse = await deleteBook(values.id);

    if (deleteResponse.statusCode == 200) {
      setErrorMessage('');
      router.back();
    } else {
      setErrorMessage(deleteResponse.errors.join('<br /><br />'));
    }
  }

  return (
    <DataLoadingView apiResponse={authorsResponse}>
      <Paper radius="md" p="xl">
        <form onSubmit={onSubmit}>
          <Stack w={300}>
            <TextInput
              required
              label="Title"
              placeholder="Title"
              radius="md"
              {...getInputProps('title')}
            />

            <NumberInput
              required
              label="Publication year"
              placeholder="Publication year"
              radius="md"
              {...getInputProps('publicationYear')}
            />

            <TextInput
              required
              label="Genre"
              placeholder="Genre"
              radius="md"
              {...getInputProps('genre')}
            />

            <NativeSelect
              required
              value={authorValue}
              label="Select author"
              placeholder="Select author"
              radius="md"
              data={authorNames}
              onChange={(ev) => {
                setAuthorValue(ev.currentTarget.value);
                setValues({
                  authorId: authorsResponse?.data
                    ? authorsResponse?.data[
                        authorNames.indexOf(ev.currentTarget.value)
                      ].id
                    : 0,
                });
              }}
            />
            {opened && (
              <Notification
                withCloseButton={false}
                title="Book was successfully updated"
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
                {upperFirst('Update')}
              </Button>
              <Button color="red" type="button" radius="md" onClick={onClick}>
                {upperFirst('Delete')}
              </Button>
            </Group>
          </Stack>
        </form>
      </Paper>
    </DataLoadingView>
  );
}
