type LoginData = {
  username: string;
  password: string;
}

export const login = async (loginData: LoginData) => {
  try {
    const response = await fetch('https://localhost:7261/api/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(loginData),
    });

    if (!response.ok) {
      throw new Error('Login failed');
    }
    const json = await response.json();

    return json.jwt;
  } catch (error) {
    console.error('Error during login:', error);
    throw error;
  }
};

type RegisterData = {
  username: string;
  password: string;
  name: string;
  surname: string;
  address: string;
  dateofbirth: Date;
}

export const register = async (registerData: RegisterData) => {
  try {
    const response = await fetch('https://localhost:7261/api/auth/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(registerData),
    });

    if (!response.ok) {
      throw new Error('Registration failed');
    }

    return await response.json();
  } catch (error) {
    console.error('Error during registration:', error);
    throw error;
  }
};

export interface UserData {
  username: string;
  name: string;
  surname: string;
  address: string;
  dateOfBirth: Date;
}

export const getUser = async () : Promise<UserData> => {
  try {
    const response = await fetch('https://localhost:7261/api/auth/user', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
      },
    });
    if (!response.ok) {
      throw new Error('GetUser failed');
    }

    const result = await response.json();
    result.dateOfBirth = new Date(result.dateOfBirth);

    return result;
  } catch (error) {
    console.error('Error during getting user:', error);
    throw error;
  }
};

export interface Author {
  id: number;
  name: string;
  surname: string;
  dateOfBirth: Date;
}

export interface Book {
  id: number;
  title: string;
  publicationYear: number;
  genre: string;
  author: Author;
}

export const getBooks = async () : Promise<Book[]> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
      },
    });

    if (!response.ok) {
      throw new Error('GetBooks failed');
    }

    const result = await response.json();
    for (let i in result){
      result[i].author.dateOfBirth = new Date(result[i].author.dateOfBirth);
    }

    return result;
  } catch (error) {
    console.error('Error during getting books:', error);
    throw error;
  }
};

export interface BookDTO {
  id: number;
  title: string;
  publicationYear: number;
  genre: string;
  authorId: number;
}

export const updateBook = async (book:BookDTO) : Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const createBook = async (book:BookDTO) : Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/book', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const deleteBook = async (id:number) : Promise<string> => {
  try {
    const response = await fetch(`https://localhost:7261/api/book?id=${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const getBook = async (id:number) : Promise<Book> => {
  try {
    const response = await fetch(`https://localhost:7261/api/book/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const getAuthors = async () : Promise<Author[]> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
      },
    });

    if (!response.ok) {
      throw new Error('GetAuthors failed');
    }

    const result = await response.json();
    for (let i in result){
      result[i].dateOfBirth = new Date(result[i].dateOfBirth);
    }

    return result;
  } catch (error) {
    console.error('Error during getting authors:', error);
    throw error;
  }
};

export const updateAuthor = async (author:Author) : Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const createAuthor = async (author:Author) : Promise<string> => {
  try {
    const response = await fetch('https://localhost:7261/api/author', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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


export const deleteAuthor = async (id:number) : Promise<string> => {
  try {
    const response = await fetch(`https://localhost:7261/api/author?id=${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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

export const getAuthor = async (id:number) : Promise<Author> => {
  try {
    const response = await fetch(`https://localhost:7261/api/author/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('jwt') ?? '')}`
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