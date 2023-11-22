'use client';

import {
  Author,
  Book,
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
  Loader,
  Notification,
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { upperFirst } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function BookDetails({ bookId }: { bookId: number }) {
  const router = useRouter();

  const [authors, setAuthors] = useState<Author[]>();
  const [book, setBook] = useState<Book>();
  const [authorValue, setAuthorValue] = useState('');
  const [opened, setOpened] = useState(false);

  const { values, getInputProps, setValues } = useForm({
    initialValues: {
      id: bookId,
      title: '',
      publicationYear: 2000,
      genre: '',
      authorId: 0,
    },

    validate: {
      title: (val) => (val.length > 0 ? null : 'This field cant be empty'),
      publicationYear: (val) =>
        val <= new Date().getFullYear()
          ? null
          : 'The field should contain a number no more then current year',
      genre: (val) => (val.length > 0 ? null : 'This field cant be empty'),
      authorId: (val) =>
        val > 0 ? null : 'Create new author before creating a book',
    },
  });

  useEffect(() => {
    getBook(bookId).then((book) => {
      setBook(book.data);
      const initalAuthorName = `${book.data?.author?.firstName} ${book.data?.author?.lastName}`;
      setAuthorValue(initalAuthorName);
      setValues({
        title: book.data?.title,
        publicationYear: book.data?.publicationYear,
        genre: book.data?.genre,
        authorId: book.data?.author?.id,
      });
    });
    getAuthors().then((authors) => {
      setAuthors(authors.data);
    });
  }, [bookId, setValues]);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      await updateBook(values);
      setOpened(true);

      setTimeout(() => setOpened(false), 2000);
    } catch (error) {
      console.error('updateBook failed:', error);
    }
  }

  async function onClick(e: React.FormEvent) {
    e.preventDefault();

    try {
      await deleteBook(values.id);

      router.back();
    } catch (error) {
      console.error('deleteBook failed:', error);
    }
  }

  if (!authors || !book) {
    return <Loader color="blue" />;
  }

  const authorsNames = authors.map((x) => `${x.firstName} ${x.lastName}`);

  return (
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
            data={authorsNames}
            onChange={(ev) => {
              setAuthorValue(ev.currentTarget.value);
              setValues({
                authorId:
                  authors[authorsNames.indexOf(ev.currentTarget.value)].id,
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
  );
}
