import { upperFirst } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import { DatePickerInput } from '@mantine/dates';
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  Button,
  Divider,
  Stack,
} from '@mantine/core';
import dayjs from 'dayjs';
import { useRouter } from 'next/navigation';
import { register } from '@/services/api';

export default function RegistrationForm() {

  const router = useRouter();

  async function onSubmit(e: React.FormEvent){
    e.preventDefault();
  
    try {
      await register(form.values);
      
      router.push('/login');
    } catch (error) {
      console.error('register failed:', error);
    }
  };

  const form = useForm({
    initialValues: {
      username: '',
      password: '',
      name: '',
      surname: '',
      dateofbirth: new Date(2000, 11, 18),
      address: '',
    },

    validate: {
      username: (val) => (val.length < 6 ? 'Username should include at least 6 characters' : null),
      password: (val) => (val.length < 6 ? 'Password should include at least 6 characters' : null),
      name: (val)=> (val.length > 0 ? null: 'This field cant be empty'),
      surname: (val)=> (val.length > 0 ? null: 'This field cant be empty'),
      dateofbirth: (val) => (dayjs(new Date()).diff(val, 'y') >= 12
      ? null : 'You should be at least 12 years old'),
      address: (val) => (val.length < 4 ? 'Address should include at least 4 characters' : null)
    },
  });

  return (
    <Paper radius='md' p='xl'>
      <Text size='lg' fw={500}>
        Welcome to bg-local.com
      </Text>

      <Divider my='lg' />

      <form onSubmit={onSubmit}>
        <Stack w={300}>

          <TextInput
            required
            label='Username'
            placeholder='Username'
            radius='md'
            {...form.getInputProps('username')}
          />

          <PasswordInput
            required
            label='Password'
            placeholder='Password'
            radius='md'
            {...form.getInputProps('password')}
          />

          <TextInput
            required
            label='Name'
            placeholder='Name'
            radius='md'
            {...form.getInputProps('name')}
          />

          <TextInput
            required
            label='Surname'
            placeholder='Surname'
            radius='md'
            {...form.getInputProps('surname')}
          />

          <DatePickerInput
            required
            label='Birthday'
            placeholder='Birthday'
            {...form.getInputProps('dateofbirth')}
          />

          <TextInput
            required
            label='Address'
            placeholder='Address'
            radius='md'
            {...form.getInputProps('address')}
          />

          <Group justify='space-between' mt='lg'>
            <Button type='submit' radius='md'>
              {upperFirst('register')}
            </Button>
          </Group>

        </Stack>
      </form>

    </Paper>)
}
