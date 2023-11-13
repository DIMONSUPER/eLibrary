'use client'

import { Author, deleteAuthor, getAuthor, updateAuthor } from '@/services/api';
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
import { upperFirst } from '@mantine/hooks';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function AuthorDetails({authorId}:{authorId:number}) {
  
  const router = useRouter();
  const [opened, setOpened] = useState(false);
  const [author, setAuthor] = useState<Author>();

  const {values, getInputProps, setValues } = useForm<Author>({
    initialValues: {
      id: authorId,
      name: '',
      surname: '',
      dateOfBirth: new Date(2000, 11, 18),
    },

    validate: {
      name: (val) => (val.length > 0 ? null: 'This field cant be empty'),
      surname: (val)=> (val.length > 0 ? null: 'This field cant be empty'),
    },
  });

  useEffect(() => {
    getAuthor(authorId)
    .then((author) => {
      console.log(author);
      setAuthor(author);
      setValues({
        name: author.name,
        surname: author.surname,
        dateOfBirth: author.dateOfBirth,
      });
    });
  }, [authorId, setValues]);

  async function onSubmit(e: React.FormEvent){
    e.preventDefault();

    try {
      await updateAuthor(values);
      setOpened(true);

      setTimeout(()=> setOpened(false), 2000);
    } catch (error) {
      console.error('updateAuthor failed:', error);
    }
  };

  async function onClick(e: React.FormEvent){
    e.preventDefault();

    try {
      await deleteAuthor(values.id);

      router.back();
    } catch (error) {
      console.error('deleteAuthor failed:', error);
    }
  };

  if (!author){
    return (
      <Loader color='blue' />
    )
  }

  return (
    <Paper radius='md' p='xl'>

      <form onSubmit={onSubmit}>
        <Stack w={300}>

          <TextInput
            required
            label='Name'
            placeholder='Name'
            radius='md'
            {...getInputProps('name')}
          />

          <TextInput
            required
            label='Surname'
            placeholder='Surname'
            radius='md'
            {...getInputProps('surname')}
          />
          
          <DatePickerInput
            required
            label='Birthday'
            placeholder='Birthday'
            {...getInputProps('dateOfBirth')}
          />

          {opened && (
            <Notification withCloseButton={false} title='Author was successfully updated' color='green'/>
          )}

          <Group justify='space-between' mt='lg'>
            <Button type='submit' radius='md'>
              {upperFirst('Update')}
            </Button>
            <Button color='red' type='button' radius='md' onClick={onClick}>
              {upperFirst('Delete')}
            </Button>
          </Group>

        </Stack>
      </form>

    </Paper>)
}