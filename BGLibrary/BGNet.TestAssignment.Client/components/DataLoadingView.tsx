'use client';

import { ApiResponse } from '@/services/api';
import { Loader } from '@mantine/core';

type DataLoadingViewProps = {
  apiResponse?: ApiResponse<any>;
  children: React.ReactNode;
};

export default function DataLoadingView({
  children,
  apiResponse,
}: DataLoadingViewProps) {
  if (!apiResponse) {
    return <Loader color="blue" />;
  } else if (apiResponse?.data == null) {
    const errorsMessage = apiResponse.errors.join('<br /><br />');

    return <p dangerouslySetInnerHTML={{ __html: errorsMessage }} />;
  } else {
    return <>{children}</>;
  }
}
