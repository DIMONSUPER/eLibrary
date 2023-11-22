const API_URL = 'https://localhost:7261/api';

export type LoginData = {
  username: string;
  password: string;
};

export type RegisterData = {
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  address: string;
  dateofbirth: Date;
};

export type UserData = {
  username: string;
  firstName: string;
  lastName: string;
  address: string;
  dateOfBirth: Date;
};

export type Author = {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
};

export type Book = {
  id: number;
  title: string;
  publicationYear: number;
  genre: string;
  author: Author;
};

export type BookDTO = {
  id: number;
  title: string;
  publicationYear: number;
  genre: string;
  authorId: number;
};

export type ApiResponse<T> = {
  statusCode: number;
  message: string;
  data: T;
  errors: string[];
};

const makePostRequest = async (path: string, body: object) => {
  var headers: { [headerName: string]: string } = {};
  headers['Content-Type'] = 'application/json';

  const jwt = `${JSON.parse(localStorage.getItem('jwt') ?? '')}`;

  if (jwt) {
    headers['Authorization'] = `Bearer ${jwt}`;
  }

  const response = await fetch(`${API_URL}/${path}`, {
    method: 'POST',
    headers: headers,
    body: JSON.stringify(body),
  });

  return (await response.json()) as ApiResponse<string>;
};

export const login = async (loginData: LoginData) => {
  try {
    const response = await fetch(`${API_URL}/auth/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(loginData),
    });

    const json = (await response.json()) as ApiResponse<string>;

    return json.data;
  } catch (error) {
    console.error('Error during login:', error);
    throw error;
  }
};

export const register = async (registerData: RegisterData) => {
  try {
    const response = await fetch(`${API_URL}/auth/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(registerData),
    });

    return (await response.json()) as ApiResponse<object>;
  } catch (error) {
    console.error('Error during registration:', error);
    throw error;
  }
};

export const getUser = async (): Promise<ApiResponse<UserData>> => {
  try {
    const response = await fetch(`${API_URL}/auth/user`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    const result = (await response.json()) as ApiResponse<UserData>;

    if (response.ok) {
      result.data.dateOfBirth = new Date(result.data.dateOfBirth);
    }

    return result;
  } catch (error) {
    console.error('Error during getting user:', error);
    throw error;
  }
};

export const getBooks = async (): Promise<Book[]> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('GetBooks failed');
    }

    const result = await response.json();
    for (let i in result) {
      result[i].author.dateOfBirth = new Date(result[i].author.dateOfBirth);
    }

    return result;
  } catch (error) {
    console.error('Error during getting books:', error);
    throw error;
  }
};

export const updateBook = async (book: BookDTO): Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
      body: JSON.stringify(book),
    });

    if (!response.ok) {
      throw new Error('UpdateBook failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during updating book:', error);
    throw error;
  }
};

export const createBook = async (book: BookDTO): Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
      body: JSON.stringify(book),
    });

    if (!response.ok) {
      throw new Error('CreateBook failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during creating book:', error);
    throw error;
  }
};

export const deleteBook = async (id: number): Promise<string> => {
  try {
    const response = await fetch(`https://localhost:7261/api/book?id=${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('DeleteBook failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during deleting book:', error);
    throw error;
  }
};

export const getBook = async (id: number): Promise<Book> => {
  try {
    const response = await fetch(`https://localhost:7261/api/book/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('GetBook failed');
    }

    const result = await response.json();
    result.author.dateOfBirth = new Date(result.author.dateOfBirth);

    return result;
  } catch (error) {
    console.error('Error during getting book:', error);
    throw error;
  }
};

export const getAuthors = async (): Promise<Author[]> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('GetAuthors failed');
    }

    const result = await response.json();
    for (let i in result) {
      result[i].dateOfBirth = new Date(result[i].dateOfBirth);
    }

    return result;
  } catch (error) {
    console.error('Error during getting authors:', error);
    throw error;
  }
};

export const updateAuthor = async (author: Author): Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
      body: JSON.stringify(author),
    });

    if (!response.ok) {
      throw new Error('UpdateAuthor failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during updating author:', error);
    throw error;
  }
};

export const createAuthor = async (author: Author): Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
      body: JSON.stringify(author),
    });

    if (!response.ok) {
      throw new Error('UpdateAuthor failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during updating author:', error);
    throw error;
  }
};

export const deleteAuthor = async (id: number): Promise<string> => {
  try {
    const response = await fetch(`https://localhost:7261/api/author?id=${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('DeleteAuthor failed');
    }

    return response.text();
  } catch (error) {
    console.error('Error during deleting author:', error);
    throw error;
  }
};

export const getAuthor = async (id: number): Promise<Author> => {
  try {
    const response = await fetch(`https://localhost:7261/api/author/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${JSON.parse(
          localStorage.getItem('jwt') ?? ''
        )}`,
      },
    });

    if (!response.ok) {
      throw new Error('GetAuthor failed');
    }

    const result = await response.json();
    result.dateOfBirth = new Date(result.dateOfBirth);

    return result;
  } catch (error) {
    console.error('Error during getting author:', error);
    throw error;
  }
};
