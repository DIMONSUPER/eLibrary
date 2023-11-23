'use client';

import { ApiResponse, Book, getBooks } from '@/services/api';
import { Table, Anchor, Stack, Button, Group } from '@mantine/core';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import DataLoadingView from '../DataLoadingView';

export default function BookList() {
  const [booksResponse, setBooksResponse] = useState<ApiResponse<Book[]>>();
  const router = useRouter();

  useEffect(() => {
    getBooks().then((booksResponse) => setBooksResponse(booksResponse));
  }, []);

  let rows: React.JSX.Element[] = [];

  if (booksResponse?.data) {
    rows = booksResponse.data.map((row) => (
      <Table.Tr key={row.id}>
        <Table.Td>
          <Anchor fz="sm" href={`/books/book/${row.id}`}>
            {row.title}
          </Anchor>
        </Table.Td>
        <Table.Td>{row.publicationYear}</Table.Td>
        <Table.Td>
          <Anchor fz="sm" href={`/authors/author/${row.author?.id}`}>
            {`${row.author?.firstName} ${row.author?.lastName}`}
          </Anchor>
        </Table.Td>
        <Table.Td>{row.genre}</Table.Td>
      </Table.Tr>
    ));
  }

  return (
    <DataLoadingView apiResponse={booksResponse}>
      <Stack>
        <Group justify="start" mt="xl">
          <Button
            type="button"
            radius="md"
            onClick={() => router.push('/books/create')}
          >
            Create book
          </Button>
          <Button
            color="yellow"
            type="button"
            radius="md"
            onClick={() => router.push('/authors/create')}
          >
            Create author
          </Button>
        </Group>
        <Table.ScrollContainer minWidth={800}>
          <Table verticalSpacing="xs">
            <Table.Thead>
              <Table.Tr>
                <Table.Th>Book title</Table.Th>
                <Table.Th>Year</Table.Th>
                <Table.Th>Author</Table.Th>
                <Table.Th>Genre</Table.Th>
              </Table.Tr>
            </Table.Thead>
            <Table.Tbody>{rows}</Table.Tbody>
          </Table>
        </Table.ScrollContainer>
      </Stack>
    </DataLoadingView>
  );
}
