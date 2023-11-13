'use client';

import { Book, getBooks } from '@/services/api';
import { Table, Anchor, Loader } from '@mantine/core';
import { useEffect, useState } from 'react';

export default function BookList() {
  const [books, setBooks] = useState<Book[]>();

  useEffect(() => {
    getBooks().then((books) => {
      setBooks(books);
    });
  }, []);

  if (!books) {
    return <Loader color="blue" />;
  }
  books.forEach((x) => console.log(x.author));
  const rows = books.map((row) => (
    <Table.Tr key={row.id}>
      <Table.Td>
        <Anchor fz="sm" href={`/books/bookdetails/${row.id}`}>
          {row.title}
        </Anchor>
      </Table.Td>
      <Table.Td>{row.publicationYear}</Table.Td>
      <Table.Td>
        <Anchor fz="sm" href={`/books/authordetails/${row.author.id}`}>
          {`${row.author.name} ${row.author.surname}`}
        </Anchor>
      </Table.Td>
      <Table.Td>{row.genre}</Table.Td>
    </Table.Tr>
  ));

  return (
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
  );
}
