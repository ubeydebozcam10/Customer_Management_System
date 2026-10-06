import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Box, Typography, Card, CardContent, Button } from '@mui/material';
import PeopleAltIcon from '@mui/icons-material/PeopleAlt';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

const Dashboard: React.FC = () => {
    const navigate = useNavigate();

    return (
        <Box
            sx={{
                minHeight: '100vh',
                background: 'linear-gradient(to bottom, #e3edf7, #f4f7fb)',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center', // Sayfayı dikeyde de tam ortalamak için eklendi
                pb: 10 // Yukarıdaki pt: 10 yerine biraz daha dengeli bir boşluk
            }}
        >
            <Typography variant="h3" color="primary" sx={{ fontWeight: 'bold', mb: 1, textAlign: 'center' }}>
                Müşteri Yönetim Sistemi
            </Typography>
            <Typography variant="subtitle1" color="text.secondary" sx={{ mb: 6, textAlign: 'center' }}>
                Lütfen yapmak istediğiniz işlemi seçin
            </Typography>

            <Box
                sx={{
                    display: 'flex',
                    flexDirection: { xs: 'column', sm: 'row' },
                    justifyContent: 'center', // Kartları yatayda merkeze hizalar
                    alignItems: 'stretch',    // İki kartın yüksekliğini birbiriyle eşitler
                    gap: 4,
                    maxWidth: 800,
                    width: '100%',
                    px: 3
                }}
            >

                {/* 1. KART: MÜŞTERİLERİ LİSTELE */}
                <Card
                    sx={{
                        flex: 1,
                        display: 'flex',
                        flexDirection: 'column', // İçeriği yukarıdan aşağı dizer
                        textAlign: 'center',
                        py: 4,
                        px: 2,
                        borderRadius: '16px',
                        transition: 'transform 0.3s ease, box-shadow 0.3s ease',
                        '&:hover': {
                            transform: 'translateY(-10px)',
                            boxShadow: '0px 12px 24px rgba(0,0,0,0.1)'
                        }
                    }}
                >
                    <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                        <PeopleAltIcon sx={{ fontSize: 60, color: '#1976d2', mb: 2 }} />
                    </Box>
                    <CardContent sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1, alignItems: 'center' }}>
                        <Typography variant="h5" sx={{ fontWeight: 'bold', mb: 1 }}>
                            Müşterileri Listele
                        </Typography>
                        <Typography variant="body2" color="text.secondary" sx={{ mb: 4 }}>
                            Kayıtlı tüm müşterileri görüntüleyin, güncelleyin veya silin.
                        </Typography>
                        <Button
                            variant="contained"
                            size="large"
                            onClick={() => navigate('/list')}
                            sx={{ borderRadius: '24px', px: 4, mt: 'auto' }} // mt: 'auto' butonu zorla en alt çizgiye hizalar
                        >
                            LİSTEYE GİT
                        </Button>
                    </CardContent>
                </Card>

                {/* 2. KART: YENİ MÜŞTERİ EKLE */}
                <Card
                    sx={{
                        flex: 1,
                        display: 'flex',
                        flexDirection: 'column',
                        textAlign: 'center',
                        py: 4,
                        px: 2,
                        borderRadius: '16px',
                        transition: 'transform 0.3s ease, box-shadow 0.3s ease',
                        '&:hover': {
                            transform: 'translateY(-10px)',
                            boxShadow: '0px 12px 24px rgba(0,0,0,0.1)'
                        }
                    }}
                >
                    <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                        <PersonAddIcon sx={{ fontSize: 60, color: '#9c27b0', mb: 2 }} />
                    </Box>
                    <CardContent sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1, alignItems: 'center' }}>
                        <Typography variant="h5" sx={{ fontWeight: 'bold', mb: 1 }}>
                            Yeni Müşteri Ekle
                        </Typography>
                        <Typography variant="body2" color="text.secondary" sx={{ mb: 4 }}>
                            Sisteme yeni bir müşteri kaydı oluşturun.
                        </Typography>
                        <Button
                            variant="outlined"
                            size="large"
                            color="secondary"
                            onClick={() => navigate('/form')}
                            sx={{ borderRadius: '24px', px: 4, borderWidth: '2px', mt: 'auto', '&:hover': { borderWidth: '2px' } }}
                        >
                            KAYIT EKLE
                        </Button>
                    </CardContent>
                </Card>

            </Box>
        </Box>
    );
};

export default Dashboard;