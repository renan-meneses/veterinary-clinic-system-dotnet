import { Routes, Route, Navigate } from 'react-router-dom'
import { useAuthStore } from '@/shared/hooks/useAuthStore'
import { AppLayout } from '@/shared/layouts/AppLayout'
import { AuthLayout } from '@/shared/layouts/AuthLayout'
import { LoginPage } from '@/features/auth/pages/LoginPage'
import { DashboardPage } from '@/features/dashboard/pages/DashboardPage'
import { UsersPage } from '@/features/users/pages/UsersPage'
import { AnimalsPage } from '@/features/animals/pages/AnimalsPage'
import { TutorPortalLayout } from '@/shared/layouts/TutorPortalLayout'
import { TutorDashboardPage } from '@/features/tutor/pages/TutorDashboardPage'
import { MonitorQueuePage } from '@/features/attendance-queue/pages/MonitorQueuePage'

function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const { isAuthenticated } = useAuthStore()

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />
  }

  return <>{children}</>
}

function App() {
  return (
    <Routes>
      <Route element={<AuthLayout />}>
        <Route path="/login" element={<LoginPage />} />
      </Route>

      <Route
        element={
          <ProtectedRoute>
            <AppLayout />
          </ProtectedRoute>
        }
      >
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/users" element={<UsersPage />} />
        <Route path="/animals" element={<AnimalsPage />} />
      </Route>

      <Route
        path="/monitor"
        element={
          <ProtectedRoute>
            <MonitorQueuePage />
          </ProtectedRoute>
        }
      />

      <Route
        path="/tutor"
        element={
          <ProtectedRoute>
            <TutorPortalLayout />
          </ProtectedRoute>
        }
      >
        <Route path="dashboard" element={<TutorDashboardPage />} />
      </Route>

      <Route path="/" element={<Navigate to="/dashboard" replace />} />
      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  )
}

export default App