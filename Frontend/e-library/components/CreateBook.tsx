import {
  Author,
  BookDTO,
  createBook,
  deleteBook,
  getAuthors,
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
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { upperFirst } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function CreateBook() {
  const router = useRouter();

  const [authors, setAuthors] = useState<Author[]>();
  const [authorValue, setAuthorValue] = useState('');

  const { values, getInputProps, setValues } = useForm<BookDTO>({
    initialValues: {
      id: 0,
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
    getAuthors().then((authors) => {
      setAuthors(authors);
      if (authors && authors.length > 0) {
        setValues({ authorId: authors[0].id });
      }
    });
  }, []);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      await createBook(values);

      router.back();
    } catch (error) {
      console.error('createBook failed:', error);
    }
  }

  if (!authors) {
    return <Loader color="blue" />;
  }

  const authorsNames = authors.map((x) => `${x.name} ${x.surname}`);

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
