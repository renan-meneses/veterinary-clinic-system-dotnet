import { Outlet } from 'react-router-dom'
import { TutorPortalHeader } from '@/shared/components/TutorPortalHeader'

export function TutorPortalLayout() {
  return (
    <div className="min-h-screen bg-gray-50">
      <TutorPortalHeader />
      <main className="container mx-auto px-4 py-6">
        <Outlet />
      </main>
    </div>
  )
}