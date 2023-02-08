import './App.css'
import { QueryClient, QueryClientProvider } from 'react-query'
import { AppHeaderbar, Layout } from './components/layout';
import { MainView } from './components/screen';
import { AuthenticationProvider } from './components/auth';

function App() {

  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <AuthenticationProvider>
      <Layout>
        <AppHeaderbar></AppHeaderbar>
        <MainView></MainView>
      </Layout>
      </AuthenticationProvider>
    </QueryClientProvider>
  )
}

export default App
