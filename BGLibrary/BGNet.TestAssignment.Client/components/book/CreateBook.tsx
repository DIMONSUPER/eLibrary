import {
  ApiResponse,
  Author,
  Book,
  createBook,
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
  Notification,
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import DataLoadingView from '../DataLoadingView';
import { useValidatedState } from '@mantine/hooks';

export default function CreateBook() {
  const router = useRouter();

  const [authorsResponse, setAuthorsResponse] =
    useState<ApiResponse<Author[]>>();
  const [authorValue, setAuthorValue] = useState('');
  const [authorNames, setAuthorNames] = useState<string[]>([]);
  const [{ value, valid }, setErrorMessage] = useValidatedState(
    '',
    (val) => val == '',
    true
  );

  const { values, getInputProps, setValues } = useForm<Book>({
    initialValues: {
      id: 0,
      title: '',
      publicationYear: 2000,
      genre: '',
      authorId: 0,
    },
  });

  useEffect(() => {
    getAuthors().then((authorsResponse) => {
      setAuthorsResponse(authorsResponse);
      if (authorsResponse?.data && authorsResponse.data.length > 0) {
        setValues({ authorId: authorsResponse.data[0].id });
        setAuthorNames(authorsResponse.data.map((x) => x.fullName!));
      }
    });
  }, [setValues]);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    const creaeteBookResponse = await createBook(values);

    if (creaeteBookResponse.statusCode == 201) {
      setErrorMessage('');
      router.back();
    } else {
      setErrorMessage(creaeteBookResponse.errors.join('<br /><br />'));
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
    </DataLoadingView>
  );
}
